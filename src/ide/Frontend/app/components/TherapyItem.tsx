import Image from 'next/image';
import TableItem from './TableItem';
import User from './User';
import Trash from '@/public/trash.svg'
import Pen from '@/public/pen.svg'
import Play from '@/public/play.svg'
import { ITherapy } from '../dashboard/therapies/page';
import { useState } from 'react';
import DeleteTherapyModal from './DeleteTherapyModal';

export function TherapyItem({therapy} : { therapy: ITherapy }) {
	const [modalVisibility, setModalVisibility] = useState(false);

	const openModal = () => {
		setModalVisibility(true);
	}

	const onSubmit = () => {
		setModalVisibility(false);
	}

	const onCancel = () => {
		setModalVisibility(false);
	}

	const onClickTrashIcon = () => {
		openModal();
	};

    return (
        <div>
			<div className='bg-white p-6 flex justify-between hover:bg-[#EAECF0] border-solid border-[#EAECF0]'>
				<div className='flex'>
					<TableItem className='w-64'>{therapy.name}</TableItem>
					<TableItem className='w-44'>{therapy.date}</TableItem>
					<TableItem className='w-44'>
						<User name={therapy.createdBy.name} username={therapy.createdBy.username}/>
					</TableItem>
					<TableItem className='w-44'>{therapy.lastExecution}</TableItem>
					<TableItem className='w-40'>{therapy.executionCount}</TableItem>
					<TableItem className='w-52'>
						<User name={therapy.lastPatient.name} username={therapy.lastPatient.username}/>
					</TableItem>
				</div>
				<TableItem className='w-40 flex justify-end'>
					<button className='cursor-pointer w-10 h-10 flex justify-center items-center hover:scale-125 duration-300' onClick={openModal}><Image src={Trash} alt='Excluir' /></button>
					<a className='cursor-pointer w-10 h-10 flex justify-center items-center hover:scale-125 duration-300' href={`/dashboard/therapies/${therapy.id}`}><Image src={Pen} alt='Editar' /></a>
					<a className='cursor-pointer w-10 h-10 flex justify-center items-center hover:scale-125 duration-300'><Image src={Play} alt='Executar' /></a>
				</TableItem>
			</div>
			{modalVisibility && (
				<DeleteTherapyModal onSubmit={onSubmit} onCancel={onCancel} />
			)}
		</div>
    );
}
