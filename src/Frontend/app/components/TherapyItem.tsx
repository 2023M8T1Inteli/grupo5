import Image from 'next/image';
import TableItem from './TableItem';
import User from './User';
import Trash from '@/public/trash.svg'
import Pen from '@/public/pen.svg'
import Play from '@/public/play.svg'
import { ITherapy } from '../dashboard/therapies/page';

export function TherapyItem({therapy} : { therapy: ITherapy }) {
    return (
        <div className='bg-white w-full p-6 flex gap-6 hover:bg-[#EAECF0] border-solid border-[#EAECF0]'>
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
            <TableItem className='w-40 flex justify-end'>
                <a className='cursor-pointer w-10 h-10 flex justify-center items-center hover:scale-125 duration-300'><Image src={Trash} alt='Excluir' /></a>
                <a className='cursor-pointer w-10 h-10 flex justify-center items-center hover:scale-125 duration-300' href={`/dashboard/therapies/${therapy.id}`}><Image src={Pen} alt='Editar' /></a>
                <a className='cursor-pointer w-10 h-10 flex justify-center items-center hover:scale-125 duration-300'><Image src={Play} alt='Executar' /></a>
            </TableItem>
        </div>
    );
}
