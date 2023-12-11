import Image from 'next/image';
import TableItem from './TableItem';
import Trash from '@/public/trash.svg'
import { ITherapist } from '../dashboard/therapists/page';
import Tag from './Tag';
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import axios from 'axios';

export function TherapistItem({therapist, onDelete} : { therapist: ITherapist, onDelete: () => void }) {

	const tagStyle = {
		'admin': {
			bgColor: '#FFF5F5',
			color: '#E7343F',
			text: 'Administrador'
		},
		'therapist': {
			bgColor: '#FDF2DA',
			color: '#F0AF26',
			text: 'Terapeuta'
		}
	}

	const deleteTherapist = async () => {
		const token = localStorage.getItem('token');
		const toastId = toast.loading('Excluindo terapeuta...');
		axios.delete(`http://localhost:80/user/id/${therapist.id}`, {
			headers: {
				'Authorization': `Bearer ${token}`
			}
		}).then(() => {
			toast.update(toastId, {
				render: 'Terapeuta excluído com sucesso!',
				type: 'success',
				autoClose: 3000,
				isLoading: false,
			});

			onDelete();
		}).catch(() => {
			toast.update(toastId, {
				render: 'Erro ao excluir terapeuta!',
				type: 'error',
				autoClose: 3000,
				isLoading: false,
			});
		});
	}

    return (
        <div className='bg-white p-6 flex justify-between hover:bg-[#EAECF0] border-solid border-[#EAECF0]'>
			<div className='flex'>
				<TableItem className='w-64'>{therapist.name}</TableItem>
				<TableItem className='w-96'>{therapist.email}</TableItem>
				<TableItem className='w-44'>
					<Tag bgColor={tagStyle[therapist.role].bgColor} text={tagStyle[therapist.role].text} color={tagStyle[therapist.role].color}/>
				</TableItem>
			</div>
            <TableItem className='flex justify-end'>
                <button onClick={deleteTherapist} className='cursor-pointer w-10 h-10 flex justify-center items-center hover:scale-125 duration-300'><Image src={Trash} alt='Excluir' /></button>
            </TableItem>
        </div>
    );
}
