'use client'
import Form, { Field } from "./Form"
import { toast, ToastContainer } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";
import axios from 'axios'
import { useRouter } from "next/navigation";
import ButtonMin from "./ButtonMin";
import Button from "./Button";
import { useForm } from "react-hook-form";

export default function LoginForm() {
	const router = useRouter();
	const fields: Field[] = [
		{
			label: 'Endereço de e-mail',
			name: 'email',
			placeholder: 'Digite seu e-mail',
			required: true,
			pattern: /^[^@]+@[^@]+\.[^@]+$/,
			type: 'email',
		},
		{
			label: 'Senha',
			name: 'password',
			placeholder: 'Digite sua senha',
			type: 'password',
			required: true,
			minLength: 8,
			maxLength: 16,
		}
	]
	const { register, handleSubmit, formState: { errors }, trigger, setValue } = useForm({
		mode: 'all',
	});
	const fillWithTestData = () => {
		setValue('email', 'carol@aacd.com.br');
		setValue('password', 'senhateste123@');
		trigger();
	};

	const onSubmit = async (data: any) => {
		const toastId = toast.loading('Realizando login...');

		console.log(data);

		try {
			const response = await axios.post('http://localhost:80/user/login', {
				email: data.email,
				password: data.password
			});

			toast.update(toastId, {
				render: 'Login realizado com sucesso!',
				type: 'success',
				autoClose: 2000,
				isLoading: false,
			});

			localStorage.setItem('token', response.data.token);

			router.push('/dashboard');
		} catch (error) {
			toast.update(toastId, {
				render: 'Erro ao realizar login!',
				type: 'error',
				autoClose: 2000,
				isLoading: false,
			});
		}

	}
	return (
		<div>
			<Form fields={fields} onSubmit={onSubmit} buttonText='Entrar' setValue={setValue} errors={errors} trigger={trigger} register={register} handleSubmit={handleSubmit} />
			<Button text="Preencher com dados de teste" style="cancel" className="w-[100%] mt-3" onClick={fillWithTestData} />
			<ToastContainer closeButton={false} />
		</div>
	)

}
