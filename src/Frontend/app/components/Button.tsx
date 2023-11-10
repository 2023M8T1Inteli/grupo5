import React from 'react';
import PropTypes from 'prop-types';

interface ButtonProps {
  text: string;
  onClick?: (e: React.MouseEvent<HTMLButtonElement>) => void;
  className?: string;
  disabled?: boolean;
}

const Button: React.FC<ButtonProps> = ({ text, onClick, className, disabled = false }) => {
  return (
    <button onClick={onClick} className={'w-full h-24 bg-[#E7343F] rounded-xl text-2xl text-white font-normal' + ' ' + className} disabled={disabled}>
      {text}
    </button>
  );
};

Button.propTypes = {
  text: PropTypes.string.isRequired,
  onClick: PropTypes.func,
  className: PropTypes.string,
  disabled: PropTypes.bool,
};

export default Button;
