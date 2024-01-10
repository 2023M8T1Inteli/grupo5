import React, { useEffect, useRef } from 'react';
import { FieldError, FieldErrors, FieldValues, UseFormHandleSubmit, UseFormRegister, UseFormSetValue, UseFormTrigger, useForm } from 'react-hook-form';
import InputText from './InputText';
import Button from './Button';
import InputSelect from './InputSelect';

export interface Field {
  label: string;
  name: string;
  placeholder: string;
  type?: string;
  value?: string;
  required?: boolean;
  pattern?: RegExp;
  minLength?: number;
  maxLength?: number;
  options?: { value: string | number; label: string }[];
	disabled?: boolean;
	defaultValue?: string;
}

interface FormProps {
  fields?: Field[];
  buttonText: string;
  onSubmit: (data: any) => void;
  cancelText?: string;
  onCancel?: () => void;
  register: UseFormRegister<FieldValues>;
  handleSubmit: UseFormHandleSubmit<FieldValues>;
  errors: FieldErrors<FieldValues>;
  trigger: UseFormTrigger<FieldValues>;
  setValue: UseFormSetValue<FieldValues>;

}

const Form: React.FC<FormProps> = ({ fields, buttonText, onSubmit, cancelText, onCancel, register, handleSubmit, errors, trigger, setValue }) => {

  const inputsRef = useRef<Array<HTMLInputElement | HTMLSelectElement | null>>([]);
  const buttonRef = useRef<HTMLButtonElement | null>(null);
  const cancelButtonRef = useRef<HTMLButtonElement | null>(null);

  useEffect(() => {
    const keydownListener = (event: KeyboardEvent) => {
      if (event.altKey) {
        if (!isNaN(Number(event.key))) {
          const index = Number(event.key) - 1;
          if (index >= 0 && index < inputsRef.current.length) {
            inputsRef.current[index]?.focus();
          }
        } else if (event.key === 'Enter') {
           if (buttonRef.current) {
			 buttonRef.current.click();
		   }
        } else if (event.key === 'c') {
			cancelButtonRef.current?.click();
		}
      }
    };
    window.addEventListener('keydown', keydownListener);
    return () => {
      window.removeEventListener('keydown', keydownListener);
    };
  }, []); // The refs should not change, so the empty array is appropriate

  useEffect(() => {
    trigger();
  }, [trigger]);

  return (
    <form className="flex flex-col gap-4 w-full h-full" onSubmit={handleSubmit(onSubmit)}>
      {fields && fields.map((field, index) => (
        <div className='w-full h-full' key={field.name}>
          {field.type !== 'select' && (
			<InputText
            label={field.label}
            placeholder={field.placeholder}
            type={field.type || 'text'}
            shortcut={`Alt+${index + 1}`}
						disabled={field.disabled}
						value={field.defaultValue}
            {...register(field.name, {
              required: field.required ? 'Este campo é obrigatório' : false,
              pattern: field.pattern ? { value: field.pattern, message: 'Formato inválido' } : undefined,
              minLength: field.minLength ? { value: field.minLength, message: `Número mínimo de caracteres não atingido` } : undefined,
              maxLength: field.maxLength ? { value: field.maxLength, message: `Número máximo de caracteres excedido` } : undefined,
            })}
			ref={(e) => {
				inputsRef.current[index] = e;
				register(field.name).ref(e);
			  }}
            error={errors[field.name]}
          />
		  )}
		  {field.type === 'select' && (
			<InputSelect
			label={field.label}
			options={field.options || []}
			shortcut={`Alt+${index + 1}`}
			{...register(field.name, {
			  required: field.required ? 'Este campo é obrigatório' : false,
			})}
			ref={(e) => {
			  inputsRef.current[index] = e;
			  register(field.name).ref(e);
			}}
			error={errors[field.name]}
		  />
		  )}
        </div>
      ))}
      <Button type='submit' ref={buttonRef} text={buttonText} shortcut='Alt+Enter' disabled={Object.keys(errors).length > 0} />
      {cancelText && <Button text={cancelText} onClick={onCancel} shortcut='Alt+C' style='cancel' ref={cancelButtonRef}/>}
    </form>
  );
};

export default Form;
