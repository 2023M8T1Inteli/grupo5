import Image from "next/image";
import ButtonCard from "../app/components/ButtonCard";
import type { Meta, StoryObj } from "@storybook/react";
import type { ButtonCardProps } from "../app/components/ButtonCard";
import EditSquare from "../public/edit_square.svg"

export default {
  title: "Components/Buttons/ButtonCard",
  component: ButtonCard,
} as Meta;

const Template: StoryObj<ButtonCardProps> = {
  render: (args) => <ButtonCard {...args} />,
};

export const Primary: StoryObj<ButtonCardProps> = {
  ...Template,
  args: {
	icon: EditSquare,
	text: "Texto do botão",
	onClick: () => console.log("Clicked!"),
  },
};



