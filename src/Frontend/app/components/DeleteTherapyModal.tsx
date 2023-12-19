import { useForm } from "react-hook-form";
import Form from "./Form";
import FormHeading from "./FormHeading";
import Modal from "./Modal";

export default function DeleteTherapyModal({onSubmit, onCancel} : { onSubmit: () => void, onCancel: () => void }) {
	const { register, handleSubmit, formState: { errors }, trigger, setValue } = useForm({
		mode: 'all',
	});
	return (
		<Modal>
			<FormHeading>Tem certeza que deseja excluir?</FormHeading>
			<Form buttonText="Excluir" onSubmit={onSubmit} cancelText="Cancelar" onCancel={onCancel} register={register} handleSubmit={handleSubmit} errors={errors} trigger={trigger} setValue={setValue}/>
		</Modal>
	)
}