import Heading from '@/app/components/Heading';
import Subheading from '../components/Subheading';
import NextPatients from '../components/NextPatients';
import Image from 'next/image';
import ButtonCard from '../components/ButtonCard';
import Heart from '@/public/whiteheart.svg'
import Profile from '@/public/profile_white.svg'

export default function Home() {
	return(
		<div className='p-16 flex flex-col gap-16'>
			<div className='grid gap-2'>
				<Heading>Início</Heading>
				<Subheading>Confira a agenda do dia, cadastre novos pacientes e crie novas terapias</Subheading>
			</div>
			<div className='flex gap-16'>
				<div className='flex flex-col gap-8'>
					<h2 className='text-4xl'>Próximos pacientes</h2>
					<div className='flex flex-col gap-4'>
						<NextPatients name='Ana Júlia' hour='13:30' data='Hoje, 16 de março de 2023' linha={" "}></NextPatients>
						<NextPatients name='Pedro Henrique' hour='15:00' data='Hoje, 16 de março de 2023'linha={" "}></NextPatients>
						<NextPatients name='Maria Luiza' hour='16:30' data='Hoje, 16 de março de 2023'linha={" "}></NextPatients>
					</div>
				</div>
				<div className='flex flex-col items-start gap-8'>
					<h2 className='text-4xl'>Atalhos</h2>
					<div className='flex gap-8'>
						<ButtonCard icon={Profile} text='Adicionar novo paciente'></ButtonCard>
						<ButtonCard icon={Heart} text='Criar nova terapia'></ButtonCard>
					</div>
				</div>
			</div>
		</div>
		

	)
}