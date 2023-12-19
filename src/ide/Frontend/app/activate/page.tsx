"use client"
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Form, { Field } from "../components/Form";
import { useForm } from "react-hook-form";
import { useEffect } from "react";
import Heading from "../components/Heading";
import { useSearchParams } from "next/navigation";
import axios from 'axios'
import { useRouter } from "next/navigation";

export default function Activate() {
	const { register, handleSubmit, formState: { errors }, trigger, setValue } = useForm({
		mode: 'all',
	});

	const searchParams = useSearchParams()
 
  const email = searchParams.get('email')?.toString();
	const token = searchParams.get('token')?.toString();

	const fields : Field[] = [
		{
			label: 'Endereço de e-mail',
			name: 'email',
			placeholder: 'Digite seu e-mail',
			required: true,
			pattern: /^[^@]+@[^@]+\.[^@]+$/,
			type: 'email',
			disabled: true,	
			defaultValue: email,
		},
		{
			label: 'Token',
			name: 'token',
			placeholder: 'Digite o token',
			required: true,
			type: 'text',
			defaultValue: token,
			disabled: true,
		},
		{
			label: 'Defina sua senha',
			name: 'password',
			placeholder: 'Digite sua senha',
			type: 'password',
			required: true,
			minLength: 8,
			maxLength: 16,
		}
		
	]

	useEffect(() => {
		setValue('email', fields[0].defaultValue);
		setValue('token', fields[1].defaultValue);
		trigger();
	// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [trigger])

	const router = useRouter();

	const onSubmit = async (data: any) => {
		console.log(data);
		const toastId = toast.loading('Ativando conta...');
		try {
			await axios.post('http://localhost:80/user/activate', {
				email: data.email,
				token: data.token,
				newpassword: data.password,
			});

			toast.update(toastId, {
				render: 'Conta ativada com sucesso!',
				type: 'success',
				autoClose: 2000,
				isLoading: false,
			});

			setTimeout(() => {
				router.push('/');
			}, 2000);


		} catch (error) {
			toast.update(toastId, {
				render: 'Erro ao ativar conta!',
				type: 'error',
				autoClose: 2000,
				isLoading: false,
			});

		}

	}

	return (
		<div className="flex justify-center items-center w-[100vw] h-[100vh]">
			<div className="w-[40rem] flex flex-col">
				<Heading className="mb-16">Ative a sua conta</Heading>
				<Form fields={fields} onSubmit={onSubmit} buttonText='Ativar conta' setValue={setValue} errors={errors} trigger={trigger} register={register} handleSubmit={handleSubmit} />
				<ToastContainer />
			</div>
		</div>
	);
}