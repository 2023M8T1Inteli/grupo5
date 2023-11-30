'use client'
import Image from 'next/image';
import TableItem from './TableItem';
import Trash from '@/public/trash.svg'
import Tag from './Tag';
import { IPatient } from '../dashboard/patients/page';

export function PatitentItem({patient} : { patient: IPatient }) {

	function diff_years(dt2 : Date, dt1 : Date) 
	{
		let diff =(dt2.getTime() - dt1.getTime()) / 1000;
		diff /= (60 * 60 * 24);
		return Math.abs(Math.floor(diff/365.25));
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
                <a className='cursor-pointer w-10 h-10 flex justify-center items-center hover:scale-125 duration-300'><Image src={Trash} alt='Excluir' /></a>
            </TableItem>
        </div>
    );
}
