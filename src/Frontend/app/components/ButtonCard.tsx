import React from 'react';
import PropTypes from 'prop-types';
import Image from 'next/image';

export interface ButtonCardProps {
  icon: any;
  text: string;
  onClick?: (e: React.MouseEvent<HTMLButtonElement>) => void;
  className?: string;
  disabled?: boolean;
}

const ButtonCard: React.FC<ButtonCardProps> = ({icon, text, onClick, className, disabled = false }) => {
  return (
    <div className='flex content-center'>
        <button onClick={onClick} className={'w-[10rem] h-[10rem] bg-[#E7343F] rounded-xl flex flex-col justify-center items-center text-1xl text-white font-normal' + ' ' + className} disabled={disabled}>
            <div className='flex flex-col gap-4 max-w-[110px] justify-center items-center'>
                
                {text}
            </div>
            
        </button>
    </div>
  );
};

ButtonCard.propTypes = {
  text: PropTypes.string.isRequired,
  onClick: PropTypes.func,
  className: PropTypes.string,
  disabled: PropTypes.bool,
};

export default ButtonCard;
