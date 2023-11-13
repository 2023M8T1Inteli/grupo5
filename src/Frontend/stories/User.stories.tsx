import User from "../app/components/User";
import type { Meta, StoryObj } from "@storybook/react";
import type { UserProps } from "../app/components/User";

export default {
  title: "Components/General/User",
  component: User,
} as Meta;

const Template: StoryObj<UserProps> = {
  render: (args) => <User {...args} />,
};

export const Primary: StoryObj<UserProps> = {
  ...Template,
  args: {
	name: "John Doe",
	username: "johndoe",
  },
};