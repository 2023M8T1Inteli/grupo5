import Subheading from "../app/components/Subheading";
import type { Meta, StoryObj } from "@storybook/react";
import type { SubheadingProps } from "../app/components/Subheading";

export default {
  title: "Components/Text/Subheading",
  component: Subheading,
} as Meta;

const Template: StoryObj<SubheadingProps> = {
  render: (args) => <Subheading {...args} />,
};

export const Primary: StoryObj<SubheadingProps> = {
  ...Template,
  args: {
	children: "Subheading",
  },
};

