import React, { useEffect, useRef } from 'react'
import { useForm } from 'react-hook-form'
import InputText from './InputText'
import Button from './Button'

export interface Field {
  label: string
  name: string
  placeholder: string
  type?: string
  validation?: Record<string, any>
}

interface FormProps {
  fields: Field[]
  buttonText: string
  onSubmit: (data: any) => void
  cancelText?: string
  onCancel ?: () => void
}

const Form: React.FC<FormProps> = ({ fields, buttonText, onSubmit, cancelText, onCancel }) => {
	const { register, handleSubmit, formState: { errors }, unregister } = useForm()
	
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

	  return (
		<form className="flex flex-col gap-4 w-full h-full" onSubmit={handleSubmit(onSubmit)}>
		  {fields.map((field, index) => (
			<div className='w-full h-full' key={index}>
			  <InputText 
				ref={(el) => inputsRef.current[index] = el}
				shortcut={`Alt+${index + 1}`}
				label={field.label} 
				placeholder={field.placeholder} 
				type={field.type || 'text'}
				name={field.name}
				/>
			  {errors[field.name] && <p className="text-red-500"> {errors[field.name]?.message?.toString()} </p>}
			</div>
		  ))}
		  <Button ref={buttonRef} text={buttonText} shortcut='Alt+Enter'/>
		  {cancelText && <Button text={cancelText} onClick={onCancel} shortcut='Esc' style='cancel'/>}
		</form>
	  )
}

export default Form
