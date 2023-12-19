import Button from '../app/components/Button';
import type { Meta, StoryObj } from '@storybook/react';
import type { ButtonProps } from '../app/components/Button'; // Assuming you have ButtonProps

export default {
  title: 'Components/Buttons/Button',
  component: Button,
} as Meta;

const Template: StoryObj<ButtonProps> = {
  render: (args) => <Button {...args} />,
};

export const Primary: StoryObj<ButtonProps> = {
  ...Template,
  args: {
    text: 'Primary',
	onClick: () => console.log('Clicked!'),
	shortcut: 'Alt + Enter',
	disabled: false,
  },
};