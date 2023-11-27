'use client';
import ButtonMin from "@/app/components/ButtonMin";
import FormHeading from "@/app/components/FormHeading";
import Heading from "@/app/components/Heading";
import Modal from "@/app/components/Modal";
import Subheading from "@/app/components/Subheading";
import { useState } from "react";
import Form, { Field } from '@/app/components/Form';
import Calendar from "@/app/components/Calendar";
import { format } from 'date-fns';
import NextPatient from "@/app/components/NextPatient";
import { InputSelectProps } from "@/app/components/InputSelect";

export default function Agenda() {
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

	const fields : Field[] | InputSelectProps[] = [
		{
			label: 'Paciente',
			name: 'patient',
			placeholder: 'Selecione um paciente',
			type: 'select',
			required: true,
			options: [
				{ value: '1', label: 'João Paulo' },
				{ value: '2', label: 'Maria Eduarda' },
				{ value: '3', label: 'José Carlos' },
				{ value: '4', label: 'Ana Maria' },
				{ value: '5', label: 'Pedro Henrique' },
			]
		},
		{
			label: 'Data',
			name: 'date',
			placeholder: 'Digite a data do atendimento',
			type: 'date',
			required: true,
		},
		{
			label: 'Horário',
			name: 'hour',
			placeholder: 'Digite o horário do atendimento',
			type: 'time',
			required: true,
		},
	]

	const events : any = {
		'2023-11-06': [{ id: 1, name: 'João Paulo', hour: '14:00', data: '10 de novembro de 2023' }],
		'2023-11-24': [{ id: 1, name: 'Maria Eduarda', hour: '13:30', data: 'Hoje, 24 de novembro de 2023' }],
	  };

	const [selectedDate, setSelectedDate] = useState<Date>(new Date());

	const handleDateSelect = (date: Date) => {
		setSelectedDate(date);
	  };
	
	  // Function to get events for the selected date
	  const getEventsForSelectedDate = () => {
		const formattedDate = format(selectedDate, 'yyyy-MM-dd');
		return events[formattedDate];
	  };



	return(
		<div className='w-[85%]'>
			<div className='flex flex-col p-16  gap-10'>
				<div className='flex justify-between items-center'>
					<div className='flex flex-col gap-2'>
						<Heading>Agenda</Heading>
						<Subheading>Confira os seus atendimentos de forma organizada</Subheading>
					</div>
					<div className='w-48'>
						<ButtonMin text='Adicionar novo' onClick={openModal} />	
					</div>
				</div>
				<div className='flex'>
					<div className='w-[40rem] mr-24'>
						<Calendar events={events} onDateSelect={handleDateSelect} />
					</div>
					<div className='flex flex-col'>
						{getEventsForSelectedDate()?.map((event : any, index : any) => (
							<NextPatient key={index} name={event.name} hour={event.hour} data={event.data} />
						))}
					</div>
				</div>

        	</div>
			{modalVisibility && (
				<Modal>
					<FormHeading>Adicionar novo evento</FormHeading>
					<Form fields={fields} buttonText="Adicionar" onSubmit={onSubmit} cancelText="Cancelar" onCancel={onCancel}/>
				</Modal>
			)}
		</div>
	)
}