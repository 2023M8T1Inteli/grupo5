import Modal from "./Modal"
import FormHeading from "./FormHeading"
import { Field } from "./Form"
import Form from "./Form"
import { useForm } from "react-hook-form";

export default function AddNewTherapyModal({onSubmit, onCancel}: { onSubmit: (data: any) => void; onCancel: () => void }) {
	const fields : Field[] = [
		{ 
		  label: 'Nome da terapia', 
		  name: 'therapy-name',
		  placeholder: 'Digite o nome da terapia',
		  type: 'text',
		  required: true,
		  minLength: 5,
		  maxLength: 50,
		},
	  ]

	const { register, handleSubmit, formState: { errors }, trigger, setValue } = useForm({
		mode: 'all',
	});
	
	return (
		<Modal>
			<FormHeading>Criar nova terapia</FormHeading>
			<Form fields={fields} buttonText="Adicionar" onSubmit={onSubmit} cancelText="Cancelar" onCancel={onCancel} register={register} handleSubmit={handleSubmit} errors={errors} trigger={trigger} setValue={setValue}/>
		</Modal>
	)
}