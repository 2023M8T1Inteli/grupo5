'use client';
import ButtonMin from "@/app/components/ButtonMin";
import FormHeading from "@/app/components/FormHeading";
import Heading from "@/app/components/Heading";
import Modal from "@/app/components/Modal";
import Subheading from "@/app/components/Subheading";
import Table from "@/app/components/Table";
import { TherapistItem } from "@/app/components/TherapistItem";
import { useState } from "react";
import Form, { Field } from '@/app/components/Form';
import { useForm } from "react-hook-form";

export interface ITherapist {
	id: string;
	name: string;
	email: string;
	role: 'therapist' | 'admin';
}

export default function Therapists() {
	const headers = [
		{ name: 'Nome', spacing: '64' },
		{ name: 'Endereço de e-mail', spacing: '64' },
		{ name: 'Cargo', spacing: '44' },
	];

	const therapists: ITherapist[] = [
		{
			id: '3bbe2596-f302-41c8-9bba-784a6e2948c0',
			name: 'Ana Carolina',
			email: 'carol@aacd.com.br',
			role: 'admin'
		},
		{
			id: '4ev496-f303-13a3-8aac-323n4b1645c9',
			name: 'Juliana Silva',
			email: 'juliana@aacd.com.br',
			role: 'therapist'
		},


	];

	const [modalVisibility, setModalVisibility] = useState(false);

	const openModal = () => {
		setModalVisibility(true);
	}

	const onSubmit = (data: any) => {
		setModalVisibility(false);
	}

	const onCancel = () => {
		setModalVisibility(false);
	}

	const fields: Field[] = [
		{
			label: 'Nome completo',
			name: 'full-name',
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
						<TherapistItem key={index} therapist={therapist} />
					))}
				</Table>
			</div>
			{modalVisibility && (
				<Modal>
					<FormHeading>Adicionar novo terapeuta</FormHeading>
					<Form fields={fields} buttonText="Adicionar" onSubmit={onSubmit} cancelText="Cancelar" onCancel={onCancel} register={register} handleSubmit={handleSubmit} errors={errors} trigger={trigger} setValue={setValue} />
				</Modal>
			)}
		</div>
	)
}