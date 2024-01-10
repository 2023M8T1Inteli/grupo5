'use client'
import Image from 'next/image';
import TableItem from './TableItem';
import Trash from '@/public/trash.svg'
import Tag from './Tag';
import { IPatient } from '../dashboard/patients/page';
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import axios from 'axios';

export function PatitentItem({patient, onDelete} : { patient: IPatient, onDelete: () => void }) {

	function diff_years(dt2 : Date, dt1 : Date) 
	{
		let diff =(dt2.getTime() - dt1.getTime()) / 1000;
		diff /= (60 * 60 * 24);
		return Math.abs(Math.floor(diff/365.25));
	}

	const deleteTherapist = async () => {
		const token = localStorage.getItem('token');
		const toastId = toast.loading('Excluindo terapeuta...');
		axios.delete(`http://localhost:80/Pacient/name/${patient.name}`, {
			headers: {
				'Authorization': `Bearer ${token}`
			}
		}).then(() => {
			toast.update(toastId, {
				render: 'Paciente excluído com sucesso!',
				type: 'success',
				autoClose: 3000,
				isLoading: false,
			});

			onDelete();
		}).catch((error) => {
			console.log(error);
			toast.update(toastId, {
				render: 'Erro ao excluir paciente!',
				type: 'error',
				autoClose: 3000,
				isLoading: false,
			});
		});
	}

    return (
        <div className='bg-white p-6 flex justify-between hover:bg-[#EAECF0] border-solid border-[#EAECF0]'>
			<div className='flex'>
				<TableItem className='w-64'>{patient.name}</TableItem>
				<TableItem className='w-64'>{diff_years(new Date(), patient.dateOfBirth)}</TableItem>
				<TableItem className='w-64'>{patient.dateOfBirth.toLocaleDateString()}</TableItem>
				<TableItem className='w-44'>Paciente</TableItem>
			</div>
            <TableItem className='flex justify-end'>
						<button onClick={deleteTherapist} className='cursor-pointer w-10 h-10 flex justify-center items-center hover:scale-125 duration-300'><Image src={Trash} alt='Excluir' /></button>
            </TableItem>
        </div>
    );
}
