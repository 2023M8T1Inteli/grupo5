'use client';
import Heading from '@/app/components/Heading';
import Subheading from '../components/Subheading';
import NextPatient from '../components/NextPatient';
import ButtonCard from '../components/ButtonCard';
import Heart from '@/public/whiteheart.svg'
import Profile from '@/public/profile_white.svg'
import { Dispatch, SetStateAction, useState } from 'react';
import AddNewPatientModal from '../components/AddNewPatientModal';
import AddNewTherapyModal from '../components/AddNewTherapyModal';

const patientsData = [
	{ name: 'Ana Júlia', hour: '13:30', data: 'Hoje, 24 de novembro de 2023' },
	{ name: 'Pedro Henrique', hour: '15:00', data: 'Hoje, 24 de novembro de 2023' },
	{ name: 'Maria Luiza', hour: '16:30', data: 'Hoje, 24 de novembro de 2023' },
];
export default function Home() {
	const [addNewPatientModalVisibility, setNewPatientModalVisibility] = useState(false);
	const [addNewTherapyModalVisibility, setNewTherapyModalVisibility] = useState(false);

	return (
		<div className='w-[85%]'>
			<div className='p-16 flex flex-col gap-16'>
				<div className='grid gap-2'>
					<Heading>Início</Heading>
					<Subheading>Confira a agenda do dia, cadastre novos pacientes e crie novas terapias</Subheading>
				</div>
				<div className='flex gap-16'>
					<div className='flex flex-col gap-8'>
						<h2 className='text-4xl'>Próximos pacientes</h2>
						<div className='flex flex-col gap-4'>
							{patientsData.map(patient => (
								<NextPatient key={patient.name} {...patient} />
							))}
						</div>
					</div>
					<div className='flex flex-col items-start gap-8'>
						<h2 className='text-4xl'>Atalhos</h2>
						<div className='flex gap-8'>
							<ButtonCard text='Adicionar novo paciente' icon={Profile} onClick={() => {setNewPatientModalVisibility(true)}}/>
							<ButtonCard text='Criar nova terapia' icon={Heart} onClick={() => {setNewTherapyModalVisibility(true)}}/>
						</div>
					</div>
				</div>
			</div>
			{addNewPatientModalVisibility && <AddNewPatientModal onSubmit={() => {setNewPatientModalVisibility(false)}} onCancel={() => {setNewPatientModalVisibility(false)}} />}
			{addNewTherapyModalVisibility && <AddNewTherapyModal onSubmit={() => {setNewPatientModalVisibility(false)}} onCancel={() => {setNewTherapyModalVisibility(false)}} />}
		</div>
	)
}
