import InputText from "../app/components/InputText";
import type { Meta, StoryObj } from "@storybook/react";
import type { InputTextProps } from "../app/components/InputText";

export default {
  title: "Components/Inputs/InputText",
  component: InputText,
} as Meta;

const Template: StoryObj<InputTextProps> = {
  render: (args) => <InputText {...args} />,
};

export const Primary: StoryObj<InputTextProps> = {
  ...Template,
  args: {
	label: "Label",
	placeholder: "Placeholder",
	type: "text",
	value: "",
	onChange: () => console.log("Changed!"),
	shortcut: "Alt + 1",
  },
};