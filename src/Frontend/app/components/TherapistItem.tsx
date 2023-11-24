import Image from 'next/image';
import TableItem from './TableItem';
import Trash from '@/public/trash.svg'
import { ITherapist } from '../dashboard/therapists/page';
import Tag from './Tag';

export function TherapistItem({therapist} : { therapist: ITherapist }) {

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

    return (
        <div className='bg-white p-6 flex justify-between hover:bg-[#EAECF0] border-solid border-[#EAECF0]'>
			<div className='flex'>
				<TableItem className='w-64'>{therapist.name}</TableItem>
				<TableItem className='w-64'>{therapist.email}</TableItem>
				<TableItem className='w-44'>
					<Tag bgColor={tagStyle[therapist.role].bgColor} text={tagStyle[therapist.role].text} color={tagStyle[therapist.role].color}/>
				</TableItem>
			</div>
            <TableItem className='flex justify-end'>
                <a className='cursor-pointer w-10 h-10 flex justify-center items-center hover:scale-125 duration-300'><Image src={Trash} alt='Excluir' /></a>
            </TableItem>
        </div>
    );
}
