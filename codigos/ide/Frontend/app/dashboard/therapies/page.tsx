'use client';
import AddNewTherapyModal from "@/app/components/AddNewTherapyModal";
import ButtonMin from "@/app/components/ButtonMin";
import Form, { Field } from "@/app/components/Form";
import FormHeading from "@/app/components/FormHeading";
import Heading from "@/app/components/Heading";
import Modal from "@/app/components/Modal";
import Subheading from "@/app/components/Subheading";
import Table from "@/app/components/Table";
import { TherapyItem } from "@/app/components/TherapyItem";
import { useEffect, useState } from "react";

export interface ITherapy {
	id: string;
	name: string;
	date: string;
	createdBy: {
		name: string;
		username: string;
	};
	lastExecution: string;
	executionCount: number;
	lastPatient: {
		name: string;
		username: string;
	};
}

export default function Therapies() {
	const [therapies, setTherapies] = useState([]);
	const BASE_API_URL = "http://localhost:80/Therapy";

	useEffect(() => {

		getAllTherapies()

	}, []);

	const createTherapy = async (name: string) => {

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
			const res = await fetch(BASE_API_URL, {
				
				headers: {
					"Content-type": "application/json",
					'Authorization': `Bearer ${token}`
			},
			method: "POST",
			body: JSON.stringify(data)
		})

		if (res.ok) {
			getAllTherapies()
			console.log("cara salvou")
		}
	}
	const getAllTherapies = async () => {

		const token = localStorage.getItem('token') ?? "";
		const res = await fetch(BASE_API_URL + "/all", {

			headers: {
				"Content-type": "application/json",
				'Authorization': `Bearer ${token}`
			},
			method: "GET",
		});

		if (res.ok) {
			const data = await res.json();
			setTherapies(data);
		}
	}
	// const therapies: ITherapy[] = [
	// 	{
	// 		id: '3bbe2596-f302-41c8-9bba-784a6e2948c0',
	// 		name: 'Terapia 1',
	// 		date: '10/10/2021',
	// 		createdBy: {
	// 			name: 'Ana Carolina',
	// 			username: 'anacarolina'
	// 		},
	// 		lastExecution: '10/10/2021',
	// 		executionCount: 10,
	// 		lastPatient: {
	// 			name: 'Maria Luiza',
	// 			username: 'marialuiza'
	// 		}
	// 	},
	// ];

	const headers = [
		{ name: 'Nome', spacing: '64' },
		{ name: 'Data de criação', spacing: '44' },
		{ name: 'Criado por', spacing: '44' },
		{ name: 'Última execução', spacing: '44' },
		{ name: 'Nº de execuções', spacing: '40' },
		{ name: 'Último paciente a executar', spacing: '52' },
	];

	const [modalVisibility, setModalVisibility] = useState(false);

	const openModal = () => {
		setModalVisibility(true);
	}

	const onSubmit = async (data: any) => {
		await createTherapy(data["therapy-name"]);
		setModalVisibility(false);
	}

	const onCancel = () => {
		setModalVisibility(false);
	}

	return (
		<div className='w-[85%]'>
			<div className='flex flex-col p-16  gap-16'>
				<div className='flex justify-between items-center'>
					<div className='flex flex-col gap-2'>
						<Heading>Terapias</Heading>
						<Subheading>Gerencie as terapias disponíveis</Subheading>
					</div>
					<div className='w-48'>
						<ButtonMin text='Criar terapia' onClick={openModal} />
					</div>
				</div>

				<Table headers={headers}>
					{therapies.map((therapy, index) => (
						<TherapyItem key={index} therapy={therapy} />
					))}
				</Table>
			</div>
			{modalVisibility && (
				<AddNewTherapyModal onCancel={onCancel} onSubmit={onSubmit} />
			)}
		</div>
	);
}
