'use client'

// Libraries
import { useState, useEffect, useRef, useMemo } from 'react';

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

export default function Terapy() {
	const BASE_API_URL = "http://localhost:80/Therapy";

    const [activeQuadrant, setActiveQuadrant] = useState(0);
    const [isToggleOn, handleToggleClick] = useToggle(false);
    const [therapyName, isEditing, handleNameClick, handleNameChange, handleNameBlur] = useTherapyName("Terapia 1");
    const [droppedItems, handleDragOver, handleDrop, changeItems] = useDrop([]);
	droppedItems.length
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
        generateRawCode(therapyName, droppedItems, commandNameMapping(activeQuadrant));
    }, [therapyName, droppedItems, activeQuadrant]);

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

    return (
        <div className='w-[85%]'>
			<div className='flex flex-col h-[100vh]'>
				<TherapyHeader {...{ therapyName, isEditing, handleNameClick, handleNameChange, handleNameBlur, isToggleOn, handleToggleClick, onClickTrashIcon }} />
				<div className='flex w-full h-[100%]'>
					<div className='flex flex-col w-[78%] h-[100%]'>
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

