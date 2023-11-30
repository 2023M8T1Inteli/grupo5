import Modal from "./Modal"
import FormHeading from "./FormHeading"
import { Field } from "./Form"
import Form from "./Form"
import { useForm } from "react-hook-form";

export default function AddNewPatientModal({onSubmit, onCancel}: { onSubmit: (data: any) => void; onCancel: () => void }) {
	const fields : Field[] = [
		{ 
		  label: 'Nome completo', 
		  name: 'full-name',
		  placeholder: 'Digite o nome completo do paciente',
		  type: 'text',
		  required: true,
		  minLength: 5,
		  maxLength: 100,
		},
		{
		  label: 'Data de nascimento',
		  name: 'date-of-birth',
		  placeholder: 'Digite a data de nascimento do paciente',
		  type: 'date',	
		  required: true,
		},
	  ]

	  const { register, handleSubmit, formState: { errors }, trigger, setValue } = useForm({
		mode: 'all',
	});

	return(
		<Modal>
			<FormHeading>Adicionar novo paciente</FormHeading>
			<Form fields={fields} buttonText="Adicionar" onSubmit={onSubmit} cancelText="Cancelar" onCancel={onCancel} register={register} handleSubmit={handleSubmit} errors={errors} trigger={trigger} setValue={setValue}/>
		</Modal>
	)
}