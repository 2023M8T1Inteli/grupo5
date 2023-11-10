import Heading from '@/app/components/Heading';
import Subheading from '../components/Subheading';
import NextPatients from '../components/NextPatients';
import Image from 'next/image';
import ButtonCard from '../components/ButtonCard';
import heart from '@/public/whiteheart.svg'
import profile from '@/public/whiteprofile.svg'

export default function Home() {
	return(
		<div className='p-16 '>
			<div className='grid gap-2'>
				<Heading>Início</Heading>
				<Subheading>Confira a agenda do dia, cadastre novos pacientes e crie novas terapias</Subheading>
			</div>
			<div className='flex space-3 gap-3 mt-8'>
				<div>
					<h2 className='text-4xl gap-3 mb-8'>Próximos pacientes</h2>
					<NextPatients name='Ana Júlia' hour='13:30' data='Hoje, 16 de março de 2023' linha={" "}></NextPatients>
					<NextPatients name='Pedro Henrique' hour='15:00' data='Hoje, 16 de março de 2023'linha={" "}></NextPatients>
					<NextPatients name='Maria Luiza' hour='16:30' data='Hoje, 16 de março de 2023'linha={" "}></NextPatients>
				</div>
				<div className='flex-col items-start'>
					<h2 className='text-4xl'>Atalhos</h2>
					<div className='flex'>
						<ButtonCard icon={profile} text='Adidionar novo paciente'></ButtonCard>
						<ButtonCard icon={heart} text='Criar nova terapia'></ButtonCard>
					</div>
				</div>
			</div>
		</div>
		

	)
}