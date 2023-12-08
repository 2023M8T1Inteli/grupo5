'use client';
import ButtonMin from "@/app/components/ButtonMin";
import Heading from "@/app/components/Heading";
import Subheading from "@/app/components/Subheading";
import Table from "@/app/components/Table";
import { useState, useEffect } from "react";
import { PatitentItem } from "@/app/components/PatientItem";
import AddNewPatientModal from "@/app/components/AddNewPatientModal";
import { ToastContainer, toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { useRouter } from "next/navigation";
import axios from "axios";

export interface IPatient {
	id: string;
	name: string;
	dateOfBirth: Date;
} 

export default function Patients() {
	const headers = [
		{name: 'Nome', spacing: '64'}, 
		{name: 'Idade', spacing: '64'},
		{name: 'Data de nascimento', spacing: '64'},
		{name: 'Cargo', spacing: '44'}
	];
	
	const [modalVisibility, setModalVisibility] = useState(false);

	const router = useRouter();

	const [patients, setPatients] = useState<IPatient[]>([]);

	function getPatients() {
		const token = localStorage.getItem('token');

		if (!token) {
			router.push('/login');
			return;
		}

		axios.get('http://localhost:80/pacient/all', {
			headers: {
				'Authorization': `Bearer ${token}`
			}
		}).then(response => {
			console.log(response.data);

			const patients: IPatient[] = response.data.map((patient: any) : IPatient => {
				return {
					id: patient.id,
					name: patient.name,
					dateOfBirth: new Date(patient.birthDate)
				}
			})

			setPatients(patients);

		});
	}

	useEffect(() => {
		getPatients();
	}, []);

	const openModal = () => {
		setModalVisibility(true);
	}

	const onSubmit = (data: any) => {
		setModalVisibility(false);
		getPatients();
	}

	const onCancel = () => {
		setModalVisibility(false);
	}

	const onDelete = () => {
		getPatients();
	}

	return(
		<div className='w-[85%]'>
			<div className='flex flex-col p-16  gap-16'>
				<div className='flex justify-between items-center'>
					<div className='flex flex-col gap-2'>
						<Heading>Pacientes</Heading>
						<Subheading>Registre, edite, acompanhe e exclua pacientes</Subheading>
					</div>
					<div className='w-48'>
						<ButtonMin text='Adicionar novo' onClick={openModal} />	
					</div>
				</div>

				<Table headers={headers}>
					{patients.map((patient, index) => (
						<PatitentItem key={index} patient={patient} onDelete={onDelete}/>
					))}
				</Table>
        	</div>
			{modalVisibility && (
				<AddNewPatientModal onCancel={onCancel} onSubmit={onSubmit} />
			)}
			<ToastContainer closeButton={false} position="bottom-right" />
		</div>
	)
}