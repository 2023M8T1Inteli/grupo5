import Heading from "../app/components/Heading";
import type { Meta, StoryObj } from "@storybook/react";
import type { HeadingProps } from "../app/components/Heading";

export default {
  title: "Components/Text/Heading",
  component: Heading,
} as Meta;

const Template: StoryObj<HeadingProps> = {
  render: (args) => <Heading {...args} />,
};

export const Primary: StoryObj<HeadingProps> = {
  ...Template,
  args: {
	children: "Heading",
  },
};

