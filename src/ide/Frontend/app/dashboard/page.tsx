'use client';
import Heading from '@/app/components/Heading';
import Subheading from '@/app/components/Subheading';
import NextPatient from '@/app/components/NextPatient';
import ButtonCard from '@/app/components/ButtonCard';
import Heart from '@/public/whiteheart.svg';
import Profile from '@/public/profile_white.svg';
import { useState, useEffect } from 'react';
import axios from 'axios';
import AddNewPatientModal from '@/app/components/AddNewPatientModal';
import AddNewTherapyModal from '@/app/components/AddNewTherapyModal';
import { format } from 'date-fns';

export default function Home() {
    const [patients, setPatients] = useState([]);
    const [addNewPatientModalVisibility, setNewPatientModalVisibility] = useState(false);
    const [addNewTherapyModalVisibility, setNewTherapyModalVisibility] = useState(false);

    useEffect(() => {
        const today = format(new Date(), 'yyyy-MM-dd');

        axios.get('http://localhost:80/pacient/all').then(response => {
            const loadedPatients = response.data.flatMap((patient:any) => {
                // Filtrar sessões para manter apenas as do dia atual
                const sessionsToday = patient.sessions.filter((session:any) => session.startedAt.startsWith(today));

                return sessionsToday.map((session:any) => ({
                    name: patient.name,
                    hour: session.startedAt.split(' ')[1],
                    data: session.startedAt.split(' ')[0],
                }));
            });
            setPatients(loadedPatients);
        });
    }, []);

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
                        <div className='flex flex-col gap-4' style={{ maxHeight: '600px', overflowY: 'auto' }}>
                        </div>
                    </div>
                    <div className='flex flex-col items-start gap-8'>
                        <h2 className='text-4xl'>Atalhos</h2>
                        <div className='flex gap-8'>
                            <ButtonCard text='Adicionar novo paciente' icon={Profile} onClick={() => { setNewPatientModalVisibility(true) }} />
                            <ButtonCard text='Criar nova terapia' icon={Heart} onClick={() => { setNewTherapyModalVisibility(true) }} />
                        </div>
                    </div>
                </div>
            </div>
            {addNewPatientModalVisibility && <AddNewPatientModal onSubmit={() => { setNewPatientModalVisibility(false) }} onCancel={() => { setNewPatientModalVisibility(false) }} />}
            {addNewTherapyModalVisibility && <AddNewTherapyModal onSubmit={() => { setNewPatientModalVisibility(false) }} onCancel={() => { setNewTherapyModalVisibility(false) }} />}
        </div>
    )
}
