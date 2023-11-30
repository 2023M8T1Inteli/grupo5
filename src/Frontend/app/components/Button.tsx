import React from 'react';
import PropTypes from 'prop-types';

export interface ButtonProps {
  text: string;
  onClick?: (e: React.MouseEvent<HTMLButtonElement>) => void;
  className?: string;
  disabled?: boolean;
  shortcut?: string;
  style?: 'primary' | 'cancel';
  type?: 'submit' | 'reset' | 'button';
}

const Button = React.forwardRef<HTMLButtonElement, ButtonProps>(	
  ({ text, onClick, className, disabled = false, shortcut, style='primary', type='button' }, ref) => {

	const styleClasses = {
		primary: 'bg-[#E7343F] text-white',
		cancel: 'bg-[#FFF] text-[#E7343F] border-[#E7343F] border-[1px]'
	}

    return (
		<div className='flex flex-col gap-2'>
			<button
				type={type}
				onClick={onClick} 
				className={'w-full h-24 font-bold rounded-xl text-lg disabled:bg-[#DDD] disabled:cursor-not-allowed ' + styleClasses[style] + ' ' + className}
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
  style: PropTypes.oneOf(['primary', 'cancel'])
};

export default Button;
