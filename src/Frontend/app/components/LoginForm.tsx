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
	const fakeLogin = (email: string, password: string) => {
		return new Promise((resolve, reject) => {
			// Simulate a network request with a timeout
			setTimeout(() => {
				if (email === 'carol@aacd.com.br' && password === 'senhateste123@') {
					resolve({ data: { message: 'Login successful' } });
				} else {
					reject(new Error('Invalid email or password'));
				}
			}, 1000);
		});
	};
	const onSubmit = async (data: any) => {
		const promise = fakeLogin(data.email, data.password);
		toast.promise(promise, {
			pending: 'Aguarde...',
			success: 'Login realizado com sucesso!',
			error: 'Erro ao realizar login!'
		});
		promise.then(() => {
			setTimeout(() => {
				router.push('/dashboard');
			}, 2000);
		}).catch((error) => {
				console.error('Login error:', error);
		});
	}
	return (
		<div>
			<Form fields={fields} onSubmit={onSubmit} buttonText='Entrar' setValue={setValue} errors={errors} trigger={trigger} register={register} handleSubmit={handleSubmit} />
			<Button text="Preencher com dados de teste" style="cancel" className="w-[100%] mt-3" onClick={fillWithTestData} />
			<ToastContainer closeButton={false} />
		</div>
	)

}
