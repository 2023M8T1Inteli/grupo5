'use client';
import ButtonMin from "@/app/components/ButtonMin";
import FormHeading from "@/app/components/FormHeading";
import Heading from "@/app/components/Heading";
import Modal from "@/app/components/Modal";
import Subheading from "@/app/components/Subheading";
import { useState, useEffect } from "react";
import Form, { Field } from '@/app/components/Form';
import Calendar from "@/app/components/Calendar";
import { format } from 'date-fns';
import NextPatient from "@/app/components/NextPatient";
import { InputSelectProps } from "@/app/components/InputSelect";
import { useForm } from "react-hook-form";
import axios from 'axios';

interface Event {
    id: number;
    name: string;
    hour: string;
    data: string;
}

interface Events {
    [key: string]: Event[];
}

export default function Agenda() {
	const [modalVisibility, setModalVisibility] = useState(false);

	const [patients, setPatients] = useState([]);

	const [events, setEvents] = useState<Events>({}); 

	const parseDateTime = (dateTimeString: { includes: (arg0: string) => any; split: (arg0: string) => [any, any]; }) => {
        let [date, time] = dateTimeString.includes('T') 
            ? dateTimeString.split('T') 
            : dateTimeString.split(' ');
        return { date, time };
    };
	
	useEffect(() => {
		axios.get('http://localhost:80/pacient/all').then(response => {
			const loadedPatients = response.data.map((patient: { id: { toString: () => any; }; name: any; }) => ({
				value: patient.id.toString(),
				label: patient.name
			}));
		
			setPatients(loadedPatients);
	
			const loadedEvents = response.data.reduce((acc: { [x: string]: { id: any; name: any; hour: any; data: any; }[]; }, patient: { sessions: any[]; name: any; }) => {
				patient.sessions.forEach((session: { startedAt: any; id: any; }) => {
					const { date, time } = parseDateTime(session.startedAt);
					acc[date] = acc[date] || [];
					acc[date].push({
						id: session.id,
						name: patient.name,
						hour: time,
						data: session.startedAt
					});
				});
				return acc;
			}, {});
			setEvents(loadedEvents);
		});
	}, []);
	

	

	const openModal = () => {
		setModalVisibility(true);
	}

	const onSubmit = async (data: { patient: any; date: any; hour: any; }) => {
		try {
			// Extrair os dados do formulário
			const { patient, date, hour } = data;
	
			// Criar objeto da sessão
			const newSession = {
				StartedAt: date + ' ' + hour, 
				EndedAt: '', 
				TherapyName: '', 
				Results: '', 
			};
			await axios.put(`http://localhost:80/pacient/addsession/${patient}`, newSession);
	
			setModalVisibility(false);
	
		} catch (error) {
			console.error('Erro ao adicionar sessão', error);
		}
		location.reload()
	};
	

	const onCancel = () => {
		setModalVisibility(false);
	}

	const fields: Field[] | InputSelectProps[] = [
		{
			label: 'Paciente',
			name: 'patient',
			placeholder: 'Selecione um paciente',
			type: 'select',
			required: true,
			options: patients,
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


	const [selectedDate, setSelectedDate] = useState<Date>(new Date());

	const handleDateSelect = (date: Date) => {
		setSelectedDate(date);
	};

	// Function to get events for the selected date
    const getEventsForSelectedDate = () => {
        const formattedDate = format(selectedDate, 'yyyy-MM-dd');
        return events[formattedDate];
    };

	const { register, handleSubmit, formState: { errors }, trigger, setValue } = useForm({
		mode: 'all',
	});

	return (
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
                    <div className='flex flex-col' style={{ maxHeight: '650px', overflowY: 'auto' }}>
                        {getEventsForSelectedDate()?.map((event, index) => (
                            <NextPatient key={index} name={event.name} hour={event.hour} data={event.data} />
                        ))}
                    </div>
				</div>

			</div>
			{modalVisibility && (
				<Modal>
					<FormHeading>Adicionar novo evento</FormHeading>
					<Form fields={fields} buttonText="Adicionar" onSubmit={onSubmit} cancelText="Cancelar" onCancel={onCancel} register={register} handleSubmit={handleSubmit} errors={errors} trigger={trigger} setValue={setValue} />
				</Modal>
			)}
		</div>
	)
}