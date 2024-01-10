'use client';
import ButtonMin from "@/app/components/ButtonMin";
import FormHeading from "@/app/components/FormHeading";
import Heading from "@/app/components/Heading";
import Modal from "@/app/components/Modal";
import Subheading from "@/app/components/Subheading";
import Table from "@/app/components/Table";
import { TherapistItem } from "@/app/components/TherapistItem";
import { useEffect, useState } from "react";
import Form, { Field } from '@/app/components/Form';
import { useForm } from "react-hook-form";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import axios from 'axios';
import { v4 as uuidv4 } from 'uuid';
import { useRouter } from "next/navigation";

export interface ITherapist {
	id: string;
	name: string;
	email: string;
	role: 'therapist' | 'admin';
}

export default function Therapists() {
	const headers = [
		{ name: 'Nome', spacing: '64' },
		{ name: 'Endereço de e-mail', spacing: '96' },
		{ name: 'Cargo', spacing: '44' },
	];

	const router = useRouter();

	const [therapists, setTherapists] = useState<ITherapist[]>([]);

	function getTherapists() {
		const token = localStorage.getItem('token');

		if (!token) {
			router.push('/login');
			return;
		}

		axios.get('http://localhost:80/user/all', {
			headers: {
				'Authorization': `Bearer ${token}`
			}
		}).then(response => {
			console.log(response.data);
			const therapists: ITherapist[] = response.data.map((therapist: any) : ITherapist => {
				return {
					id: therapist._id,
					name: therapist.Name,
					email: therapist.Email,
					role: therapist.Role
				}
			});
			setTherapists(therapists);
		});
	}

	useEffect(() => {
		getTherapists();
	}, []);

	const [modalVisibility, setModalVisibility] = useState(false);

	const openModal = () => {
		setModalVisibility(true);
	}

	const onSubmit = async (data: any) => {
		const token = localStorage.getItem('token');
		const toastId = toast.loading('Adicionando terapeuta...');
		try {
			await axios.post('http://localhost:80/user/', {
				name: data.fullName,
				email: data.email
			}, {
				headers: {
					'Authorization': `Bearer ${token}`
				}
			});

			toast.update(toastId, {
				render: 'Terapeuta adicionado com sucesso!',
				type: 'success',
				autoClose: 2000,
				isLoading: false,
			});

			setModalVisibility(false);

			getTherapists();

		} catch (error) {
			toast.update(toastId, {
				render: 'Erro ao adicionar terapeuta!',
				type: 'error',
				autoClose: 2000,
				isLoading: false,
			});
		}
	}

	const onCancel = () => {
		setModalVisibility(false);
	}

	const fields: Field[] = [
		{
			label: 'Nome completo',
			name: 'fullName',
			placeholder: 'Digite o nome completo do terapeuta',
			minLength: 5,
			maxLength: 100,
			required: true,
		},
		{
			label: 'E-mail',
			name: 'email',
			placeholder: 'Digite o e-mail do terapeuta',
			type: 'email',
			required: true,
			pattern: /^[^@]+@[^@]+\.[^@]+$/
		},
	]
	const { register, handleSubmit, formState: { errors }, trigger, setValue } = useForm({
		mode: 'all',
	});
	return (
		<div className='w-[85%]'>
			<div className='flex flex-col p-16  gap-16'>
				<div className='flex justify-between items-center'>
					<div className='flex flex-col gap-2'>
						<Heading>Terapeutas</Heading>
						<Subheading>Registre, edite e exclua terapeutas e administradores</Subheading>
					</div>
					<div className='w-48'>
						<ButtonMin text='Adicionar novo' onClick={openModal} />
					</div>
				</div>

				<Table headers={headers}>
					{therapists.map((therapist, index) => (
						<TherapistItem key={index} therapist={therapist} onDelete={getTherapists}/>
					))}
				</Table>
			</div>
			{modalVisibility && (
				<Modal>
					<FormHeading>Adicionar novo terapeuta</FormHeading>
					<Form fields={fields} buttonText="Adicionar" onSubmit={onSubmit} cancelText="Cancelar" onCancel={onCancel} register={register} handleSubmit={handleSubmit} errors={errors} trigger={trigger} setValue={setValue} />
				</Modal>
			)}
			<ToastContainer closeButton={false} />
		</div>
	)
}