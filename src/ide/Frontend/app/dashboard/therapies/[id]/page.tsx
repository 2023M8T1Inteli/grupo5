'use client'

// Libraries
import { useState, useEffect, useRef, useMemo, ChangeEvent } from 'react';

// Hooks
import useDrop from '@/app/hooks/useDrop';
import useToggle from '@/app/hooks/useToggle';
import useTherapyName from '@/app/hooks/useTherapyName';

// Components
import BoardIcons from '@/app/components/BoardIcons';
import DroppedItems from '@/app/components/DroppedItems';
import TherapyHeader from '@/app/components/TherapyHeader';
import DraggableItems from '@/app/components/DraggableItems';

// Helpers and constants
import * as Boards from '@/public/boards';
import chunkArray from '@/app/helpers/chuckArray';
import { commandNameMapping, generateRawCode } from '@/app/helpers/codeGenerator';
import Modal from '@/app/components/Modal';
import FormHeading from '@/app/components/FormHeading';
import { Field } from 'react-hook-form';
import Form from '@/app/components/Form';
import DeleteTherapyModal from '@/app/components/DeleteTherapyModal';
import awsConfig from '../../../../AWS.json';
import AWS from 'aws-sdk';

AWS.config.update({
    accessKeyId: awsConfig.accessKeyId, 
    secretAccessKey: awsConfig.secretAccessKey, 
    region: awsConfig.region
});


const s3 = new AWS.S3();


export default function Terapy() {
	const BASE_API_URL = "http://localhost:80/Therapy";

    const [activeQuadrant, setActiveQuadrant] = useState(0);
    const [isToggleOn, handleToggleClick] = useToggle(false);
    const [therapyName, isEditing, handleNameClick, handleNameChange, handleNameBlur] = useTherapyName("Terapia 1");
    const [droppedItems, handleDragOver, handleDrop, changeItems] = useDrop([]);
	const [imageFile, setImageFileUrl] = useState<string | null>(null);
	const [audioFile, setAudioFileUrl] = useState<string | null>(null);
	
	
    const handleQuadrantClick = (index : number) => {
        setActiveQuadrant(index);
    };

	const fetchBlocks = ()=>{
		
		changeItems([]);
	}

	const createTherapyBlocks = async (name: string) => {

		const data = {
			"name": name,
			"createdByUser": "admin",
			"command": []
		};
		const token = localStorage.getItem('token') ?? "";
		// const toastId = toast.loading('Excluindo terapeuta...');
		// axios.delete(`http://localhost:80/Pacient/name/${patient.name}`, {
		// 	headers: {
		// 	}
			const res = await fetch(BASE_API_URL+"/name/"+{name}, {
				
				headers: {
					"Content-type": "application/json",
					'Authorization': `Bearer ${token}`
			},
			method: "PATCH",
			body: JSON.stringify(data)
		})

		if (res.ok) {
			// getAllTherapies()
			// console.log("cara salvou")
		}
	}
	
	useEffect(() => {
		if (imageFile && audioFile) {
			generateRawCode(therapyName, droppedItems, commandNameMapping(activeQuadrant), imageFile, audioFile);
		}
	}, [therapyName, droppedItems, activeQuadrant, imageFile, audioFile]);
	
	const boardIcons = Object.values(Boards);
    const chunkedBoardIcons = useMemo(() => chunkArray(boardIcons, 3), [boardIcons]);

	const [modalVisibility, setModalVisibility] = useState(false);

	const openModal = () => {
		setModalVisibility(true);
	}

	const onSubmit = () => {
		setModalVisibility(false);
	}

	const onCancel = () => {
		setModalVisibility(false);
	}

	const onClickTrashIcon = () => {
		openModal();
	};

	const handleFileChange = async (e: ChangeEvent<HTMLInputElement>, fileType: string) => {
		if (e.target.files && e.target.files[0]) {
			const file = e.target.files[0];
			const buffer = await fileToBuffer(file);
			const fileUrl = await uploadToS3(buffer, file, fileType);
			if (fileType === 'image') {
				setImageFileUrl(fileUrl);
			} else if (fileType === 'audio') {
				setAudioFileUrl(fileUrl);
			}
		}
	};

	const fileToBuffer = async (file: File): Promise<Buffer> => {
		return new Promise((resolve, reject) => {
			const reader = new FileReader();
			reader.onload = () => {
				const arrayBuffer = reader.result as ArrayBuffer;
				const buffer = Buffer.from(arrayBuffer);
				resolve(buffer);
			};
			reader.onerror = (err) => {
				reject(err);
			};
			reader.readAsArrayBuffer(file);
		});
	};
	
	const uploadToS3 = async (file: any, fileName: File, fileType: string) => {
		console.log(file);
		const params = {
			Bucket: 'terapia',
			Key: `${fileType}/${fileName.name}`,
			Body: file,
		};
	
		try {
			const data = await s3.upload(params).promise();
			return data.Location; 
		} catch (err) {
			console.error("Erro no upload para o S3:", err);
			return null;
		}
	};
	
    return (
        <div className='w-[85%]'>
			<div className='flex flex-col h-[100vh]'>
				<TherapyHeader {...{ therapyName, isEditing, handleNameClick, handleNameChange, handleNameBlur, isToggleOn, handleToggleClick, onClickTrashIcon }} />

				<div className='flex w-full h-[100%]'>
					<div className='flex flex-col w-[78%] h-[100%]'>
					<div style={{ display: 'flex', justifyContent: 'flex-end', gap: '10px' }}>
					<label
						htmlFor="imageInput"
						style={{
						display: 'inline-block',
						padding: '10px',
						backgroundColor: 'rgb(231, 52, 63)', 
						color: '#fff',
						borderRadius: '5px',
						cursor: 'pointer',
						transition: 'background-color 0.2s ease-in-out',
						}}
					>
						Escolher imagem
						<input
						type="file"
						accept="image/*"
						id="imageInput"
						onChange={(e) => handleFileChange(e, 'image')}
						style={{
							display: 'none', 
						}}
						/>
					</label>
					<label
						htmlFor="audioInput"
						style={{
						display: 'inline-block',
						padding: '10px',
						backgroundColor: 'rgb(231, 52, 63)', 
						color: '#fff',
						borderRadius: '5px',
						cursor: 'pointer',
						transition: 'background-color 0.2s ease-in-out',
						}}
					>
						Escolher áudio
						<input
						type="file"
						accept="audio/*"
						id="audioInput"
						onChange={(e) => handleFileChange(e, 'audio')}
						style={{
							display: 'none', 
						}}
						/>
					</label>
					</div>

						<DraggableItems droppedItems={droppedItems} />
						<DroppedItems {...{ droppedItems, handleDragOver, handleDrop }} />
					</div>
					<BoardIcons {...{ handleQuadrantClick, activeQuadrant, chunkedBoardIcons }} />
				</div>
        	</div>
			{modalVisibility && (
				<DeleteTherapyModal onSubmit={onSubmit} onCancel={onCancel} />
			)}
		</div>
    )
}

