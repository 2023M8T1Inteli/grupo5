import Form from "./Form";
import FormHeading from "./FormHeading";
import Modal from "./Modal";

export default function DeleteTherapyModal({onSubmit, onCancel} : { onSubmit: () => void, onCancel: () => void }) {
	return (
		<Modal>
			<FormHeading>Tem certeza que deseja excluir?</FormHeading>
			<Form buttonText="Excluir" onSubmit={onSubmit} cancelText="Cancelar" onCancel={onCancel}/>
		</Modal>
	)
}