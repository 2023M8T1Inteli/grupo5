import Sidebar from "../app/components/Sidebar";
import type { Meta, StoryObj } from "@storybook/react";
import type { SidebarProps } from "../app/components/Sidebar";

export default {
  title: "Components/General/Sidebar",
  component: Sidebar,
} as Meta;

const Template: StoryObj<SidebarProps> = {
  render: (args) => <Sidebar {...args} />,
};

export const Primary: StoryObj<SidebarProps> = {
  ...Template,
  args: {
  },
};
