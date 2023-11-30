'use client'
import Form, { Field } from "./Form"
import { toast, ToastContainer } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";
import axios from 'axios'
import { useRouter } from "next/navigation";

export default function LoginForm() {
	const router = useRouter();

	const fields: Field[] = [
		{
			label: 'Endereço de e-mail',
			name: 'email',
			placeholder: 'Digite seu e-mail',
			required: true,
			// pattern: /^[^@]+@[^@]+\.[^@]+$/,
			type: 'text', // TO-DO: alterar para 'email' quando a integração com o backend estiver pronta.
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

	const onSubmit = async (data: any) => {
		console.log(data)
		// TO-DO: implementar a lógica de login.
		const promise = axios.post('https://dummyjson.com/auth/login', {
			username: data.email,
			password: data.password
		}, {
			headers: {
				'Content-Type': 'application/json'
			}
		})

		toast.promise(promise, {
			pending: 'Aguarde...',
			success: 'Login realizado com sucesso!',
			error: 'Erro ao realizar login!'
		})

		promise.then(() => {
			setTimeout(() => {
				router.push('/dashboard');
			}, 2000)
		})
		.catch((error) => {
			console.error('Login error:', error);
		});	
	}

	return (
		<div>
			<Form fields={fields} onSubmit={onSubmit} buttonText='Entrar' />
			<ToastContainer closeButton={false} />
		</div>
	)

}
