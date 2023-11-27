import React, { useEffect, useRef } from 'react'
import { useForm } from 'react-hook-form'
import InputText from './InputText'
import Button from './Button'

export interface Field {
	label: string
	name: string
	placeholder: string
	type?: string
	value?: string
	required?: boolean
	pattern?: RegExp
	minLength?: number
	maxLength?: number
}

interface FormProps {
	fields?: Field[]
	buttonText: string
	onSubmit: (data: any) => void
	cancelText?: string
	onCancel?: () => void
}

const Form: React.FC<FormProps> = ({ fields, buttonText, onSubmit, cancelText, onCancel }) => {
	const { register, handleSubmit, formState: { errors }, trigger } = useForm({
		mode: 'all',
	});

	const inputsRef = useRef<Array<HTMLInputElement | null>>([])
	const buttonRef = useRef<HTMLButtonElement | null>(null)

	useEffect(() => {
		const keydownListener = (event: KeyboardEvent) => {
			if (event.altKey) {
				if (!isNaN(Number(event.key))) {
					const index = Number(event.key) - 1
					if (index >= 0 && index < inputsRef.current.length) {
						inputsRef.current[index]?.focus()
					}
				} else if (event.key === 'Enter') {
					buttonRef.current?.focus()
				}
			}
		}
		window.addEventListener('keydown', keydownListener)
		return () => {
			window.removeEventListener('keydown', keydownListener)
		}
	}, [])

	useEffect(() => {
		trigger();
	}, [trigger]);

	return (
		<form className="flex flex-col gap-4 w-full h-full" onSubmit={handleSubmit(onSubmit)}>
			{fields && fields.map((field, index) => (
				<div className='w-full h-full' key={field.name}>
					<InputText
						label={field.label}
						placeholder={field.placeholder}
						type={field.type || 'text'}
						{...register(field.name, {
							required: field.required ? 'Este campo é obrigatório' : false,
							pattern: field.pattern ? { value: field.pattern, message: 'Formato inválido' } : undefined,
							minLength: field.minLength ? { value: field.minLength, message: `Número mínimo de caracteres não atingido` } : undefined,
							maxLength: field.maxLength ? { value: field.maxLength, message: `Número máximo de caracteres excedido` } : undefined,
						})}
						error={errors[field.name]}
					/>
				</div>
			))}
			<Button type='submit' ref={buttonRef} text={buttonText} shortcut='Alt+Enter' disabled={Object.keys(errors).length > 0} />
			{cancelText && <Button text={cancelText} onClick={onCancel} shortcut='Esc' style='cancel' />}
		</form>
	)
}

export default Form
