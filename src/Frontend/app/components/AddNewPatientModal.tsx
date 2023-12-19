import React from "react";
import Modal from "./Modal";
import FormHeading from "./FormHeading";
import { Field } from "./Form";
import Form from "./Form";
import { useForm } from "react-hook-form";
import axios from "axios";
import { toast } from "react-toastify";

export default function AddNewPatientModal({
  onCancel,
  onSubmit,
}: {
  onCancel: () => void;
  onSubmit: (data: any) => void;
}) {
  const fields: Field[] = [
    {
      label: "Nome completo",
      name: "fullName",
      placeholder: "Digite o nome completo do paciente",
      type: "text",
      required: true,
      minLength: 5,
      maxLength: 100,
    },
    {
      label: "Data de nascimento",
      name: "dateOfBirth",
      placeholder: "Digite a data de nascimento do paciente",
      type: "date",
      required: true,
    },
  ];

  const {
    register,
    handleSubmit,
    formState: { errors },
    trigger,
    setValue,
  } = useForm({
    mode: "all",
  });

  const onSubmitPatient = async (data: any) => {
    console.log(data);
    const token = localStorage.getItem("token");

    const toastId = toast.loading("Adicionando paciente...");
    try {
      await axios.post(
        "http://localhost:80/pacient/",
        {
          Name: data["fullName"],
          BirthDate: data["dateOfBirth"],
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      toast.update(toastId, {
        render: "Paciente adicionado com sucesso!",
        type: "success",
        autoClose: 2000,
        isLoading: false,
      });

      onSubmit(data);
    } catch (error) {
      toast.update(toastId, {
        render: "Erro ao adicionar paciente!",
        type: "error",
        autoClose: 2000,
        isLoading: false,
      });
    }
  };

  return (
    <Modal>
      <FormHeading>Adicionar novo paciente</FormHeading>
      <Form
        fields={fields}
        buttonText="Adicionar"
        onSubmit={handleSubmit(onSubmitPatient)}
        cancelText="Cancelar"
        onCancel={onCancel}
        register={register}
        errors={errors}
        trigger={trigger}
        setValue={setValue}
        handleSubmit={handleSubmit}
      />
    </Modal>
  );
}
