'use client'
import Form, { Field } from "./Form"

export default function LoginForm() {
	const fields : Field[] = [
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

	const onSubmit = (data : any) => {
		console.log(data)
		// TO-DO: implementar a lógica de login.
	}

	return (
		<Form fields={fields} onSubmit={onSubmit} buttonText='Entrar' />
	)

}
	