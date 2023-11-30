import Modal from "./Modal"
import FormHeading from "./FormHeading"
import { Field } from "./Form"
import Form from "./Form"

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
	
	return (
		<Modal>
			<FormHeading>Criar nova terapia</FormHeading>
			<Form fields={fields} buttonText="Adicionar" onSubmit={onSubmit} cancelText="Cancelar" onCancel={onCancel}/>
		</Modal>
	)
}