'use client'
import ButtonMin from '@/app/components/ButtonMin';
import Heading from '@/app/components/Heading';
import Subheading from '@/app/components/Subheading';
import TableHeadItem from '@/app/components/TableHeadItem';
import TableItem from '@/app/components/TableItem';
import Trash from '@/public/trash.svg'
import Pen from '@/public/pen.svg'
import Play from '@/public/play.svg'
import Image from 'next/image';
import User from '@/app/components/User';

export default function Therapies() {
	return(
		<div className='flex flex-col p-16 w-full gap-16'>
			<div className='flex justify-between items-center'>
				<div className='flex flex-col gap-2'>
					<Heading>Terapias</Heading>
					<Subheading>Gerencie as terapias disponíveis</Subheading>
				</div>
				<div className='w-48'>
					<ButtonMin text='Criar terapia'/>	
				</div>
			</div>

			<div className='rounded-lg border-solid border-2 border-[#EAECF0] overflow-hidden'>
				<div className='bg-[#F9FAFB] w-full px-6 py-3 flex gap-6'>
					<TableHeadItem className='w-64'>Nome</TableHeadItem>
					<TableHeadItem className='w-44'>Data de criação</TableHeadItem>
					<TableHeadItem className='w-44'>Criado por</TableHeadItem>
					<TableHeadItem className='w-44'>Última execução</TableHeadItem>
					<TableHeadItem className='w-40'>Nº de execuções</TableHeadItem>
					<TableHeadItem className='w-52'>Último paciente a executar</TableHeadItem>
				</div>
				<div className='bg-white w-full p-6 flex gap-6 hover:bg-[#EAECF0] border-solid border-b border-[#EAECF0]'>
					<TableItem className='w-64'>Terapia 1</TableItem>
					<TableItem className='w-44'>10/10/2021</TableItem>
					<TableItem className='w-44'>
						<User name='Ana Carolina' username='anacarolina'/>
					</TableItem>
					<TableItem className='w-44'>10/10/2021</TableItem>
					<TableItem className='w-40'>10</TableItem>
					<TableItem className='w-52'>
						<User name='Maria Luiza' username='marialuiza'/>
					</TableItem>
					<TableItem className='w-40 flex justify-end'>
						<a className='cursor-pointer w-10 h-10 flex justify-center items-center'><Image src={Trash} alt='Excluir' /></a>
						<a className='cursor-pointer w-10 h-10 flex justify-center items-center'><Image src={Pen} alt='Editar' /></a>
						<a className='cursor-pointer w-10 h-10 flex justify-center items-center' href='/dashboard/therapies/dec7a9bd-e265-4100-a8e2-3cd9a881af3c'><Image src={Play} alt='Executar' /></a>
					</TableItem>
				</div>
				<div className='bg-white w-full p-6 flex gap-6 hover:bg-[#EAECF0] border-solid border-[#EAECF0]'>
					<TableItem className='w-64'>Terapia 2</TableItem>
					<TableItem className='w-44'>22/03/2023</TableItem>
					<TableItem className='w-44'>
						<User name='Carol Braga' username='carolbraga'/>
					</TableItem>
					<TableItem className='w-44'>05/11/2023</TableItem>
					<TableItem className='w-40'>344</TableItem>
					<TableItem className='w-52'>
						<User name='João Pedro' username='joaopedro'/>
					</TableItem>
					<TableItem className='w-40 flex justify-end'>
						<a className='cursor-pointer w-10 h-10 flex justify-center items-center'><Image src={Trash} alt='Excluir' /></a>
						<a className='cursor-pointer w-10 h-10 flex justify-center items-center'><Image src={Pen} alt='Editar' /></a>
						<a className='cursor-pointer w-10 h-10 flex justify-center items-center'><Image src={Play} alt='Executar' /></a>
					</TableItem>
				</div>
			</div>


		</div>
	)
}