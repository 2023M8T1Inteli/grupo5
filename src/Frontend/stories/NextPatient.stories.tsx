import NextPatient from "../app/components/NextPatient";
import type { Meta, StoryObj } from "@storybook/react";
import type { NextPatientProps } from "../app/components/NextPatient";

export default {
  title: "Components/General/NextPatient",
  component: NextPatient,
} as Meta;

const Template: StoryObj<NextPatientProps> = {
  render: (args) => <NextPatient {...args} />,
};

export const Primary: StoryObj<NextPatientProps> = {
  ...Template,
  args: {
	name: "Nome do Paciente",
	hour: "10:00",
	data: "10/10/2021",
  },
};