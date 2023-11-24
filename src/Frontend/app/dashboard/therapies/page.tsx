'use client';
import ButtonMin from "@/app/components/ButtonMin";
import Form from "@/app/components/Form";
import FormHeading from "@/app/components/FormHeading";
import Heading from "@/app/components/Heading";
import Modal from "@/app/components/Modal";
import Subheading from "@/app/components/Subheading";
import Table from "@/app/components/Table";
import { TherapyItem } from "@/app/components/TherapyItem";
import { useState } from "react";

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
    const therapies : ITherapy[] = [
        {
			id: '3bbe2596-f302-41c8-9bba-784a6e2948c0',
            name: 'Terapia 1',
            date: '10/10/2021',
            createdBy: {
                name: 'Ana Carolina',
                username: 'anacarolina'
            },
            lastExecution: '10/10/2021',
            executionCount: 10,
            lastPatient: {
                name: 'Maria Luiza',
                username: 'marialuiza'
            }
        },
    ];

    const headers = [
		{name: 'Nome', spacing: '64'}, 
		{name: 'Data de criação', spacing: '44'},
		{name: 'Criado por', spacing: '44'},
		{name: 'Última execução', spacing: '44'},
		{name: 'Nº de execuções', spacing: '40'},
		{name: 'Último paciente a executar', spacing: '52'},
		{name: '', spacing: '40'}
	];

	const fields = [
		{ 
		  label: 'Nome da terapia', 
		  name: 'therapy-name',
		  placeholder: 'Digite o nome da terapia',
		},
	  ]

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
				<Modal>
					<FormHeading>Criar nova terapia</FormHeading>
					<Form fields={fields} buttonText="Adicionar" onSubmit={onSubmit} cancelText="Cancelar" onCancel={onCancel}/>
				</Modal>
			)}
		</div>
    );
}
