'use client'
import Form from "./Form"

export default function LoginForm() {
	const fields = [
		{ 
		  label: 'Endereço de e-mail', 
		  name: 'email',
		  placeholder: 'Digite seu e-mail', 
		  validation: { required: 'O email é obrigatório', pattern: { value: /^\S+@\S+$/i, message: 'Email inválido' } } 
		},
		{ 
		  label: 'Senha', 
		  name: 'password',
		  placeholder: 'Digite sua senha', 
		  type: 'password',
		  validation: { required: 'A senha é obrigatória' } 
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
	