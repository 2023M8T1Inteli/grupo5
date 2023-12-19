import ButtonMin from '../app/components/ButtonMin';
import type { Meta, StoryObj } from '@storybook/react';
import type { ButtonMinProps } from '../app/components/ButtonMin';

export default {
  title: 'Components/Buttons/ButtonMin',
  component: ButtonMin,
} as Meta;

const Template: StoryObj<ButtonMinProps> = {
  render: (args) => <ButtonMin {...args} />,
};

export const Primary: StoryObj<ButtonMinProps> = {
  ...Template,
  args: {
    text: 'Primary',
	onClick: () => console.log('Clicked!'),
	disabled: false,
  },
};