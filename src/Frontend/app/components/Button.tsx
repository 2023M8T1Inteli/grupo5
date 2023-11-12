import React from 'react';
import PropTypes from 'prop-types';

interface ButtonProps {
  text: string;
  onClick?: (e: React.MouseEvent<HTMLButtonElement>) => void;
  className?: string;
  disabled?: boolean;
  shortcut?: string;
}

const Button = React.forwardRef<HTMLButtonElement, ButtonProps>(
  ({ text, onClick, className, disabled = false, shortcut }, ref) => {
    return (
		<div className='flex flex-col gap-2'>
			<button
				onClick={onClick} 
				className={'w-full h-24 bg-[#E7343F] rounded-xl text-2xl text-white font-normal' + ' ' + className} 
				disabled={disabled}
				ref={ref}
				title={shortcut ? `Shortcut: ${shortcut}` : ''}
			>
				{text}
			</button>
			<p className='w-fit text-sm text-[#909090] bg-[#f6f8fa] p-1 rounded'>{shortcut}</p>
		</div>
    );
  }
);

Button.displayName = "SubmitButton";

Button.propTypes = {
  text: PropTypes.string.isRequired,
  onClick: PropTypes.func,
  className: PropTypes.string,
  disabled: PropTypes.bool,
  shortcut: PropTypes.string,
};

export default Button;
