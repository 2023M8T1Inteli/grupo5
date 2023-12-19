import React from 'react';
import PropTypes from 'prop-types';
import Image from 'next/image';

export interface ButtonMinProps {
  icon ?: any;
  text: string;
  onClick?: (e: React.MouseEvent<HTMLButtonElement>) => void;
  className?: string;
  disabled?: boolean;
}

const ButtonMin: React.FC<ButtonMinProps> = ({ icon, text, onClick, className, disabled = false }) => {
  return (
    <button onClick={onClick} className={'w-full h-14 bg-[#E7343F] rounded-lg text-base text-white font-normal px-8 py-4 hover:scale-105 duration-300 flex justify-center items-center gap-4' + ' ' + className} disabled={disabled}>
	  {icon && <Image src={icon} alt='Ícone'/>}
      {text}
    </button>
  );
};

ButtonMin.propTypes = {
  text: PropTypes.string.isRequired,
  onClick: PropTypes.func,
  className: PropTypes.string,
  disabled: PropTypes.bool,
};

export default ButtonMin;
