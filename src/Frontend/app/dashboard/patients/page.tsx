'use client';
import ButtonMin from "@/app/components/ButtonMin";
import FormHeading from "@/app/components/FormHeading";
import Heading from "@/app/components/Heading";
import Modal from "@/app/components/Modal";
import Subheading from "@/app/components/Subheading";
import Table from "@/app/components/Table";
import { TherapistItem } from "@/app/components/TherapistItem";
import { useState } from "react";
import Form, { Field } from '@/app/components/Form';
import { PatitentItem } from "@/app/components/PatientItem";
import AddNewPatientModal from "@/app/components/AddNewPatientModal";

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

	const patients : IPatient[] = [
        {
			id: '3bbe2596-f302-41c8-9bba-784a6e2948c0',
            name: 'João Paulo',
			dateOfBirth: new Date('2016-05-09'),
		},
    ];
	
	const [modalVisibility, setModalVisibility] = useState(false);

	const openModal = () => {
		setModalVisibility(true);
	}

	const onSubmit = (data: any) => {
		setModalVisibility(false);
	}

	const onCancel = () => {
		setModalVisibility(false);
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
						<PatitentItem key={index} patient={patient} />
					))}
				</Table>
        	</div>
			{modalVisibility && (
				<AddNewPatientModal onCancel={onCancel} onSubmit={onSubmit} />
			)}
		</div>
	)
}