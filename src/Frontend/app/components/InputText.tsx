import React from 'react';
import PropTypes from 'prop-types';

interface InputTextProps {
  label?: string;
  placeholder?: string;
  type?: string;
  value?: string | number;
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
  className?: string;
}

const InputText: React.FC<InputTextProps> = ({ label, placeholder, type = 'text', value, onChange, className }) => {
  return (
    <div className={'flex flex-col gap-2' + ' ' + className}>
      {label && <label className='text-xl text-[#909090]'>{label}</label>}
      <input
        type={type}
        placeholder={placeholder}
        value={value}
        onChange={onChange}
		className='w-full h-24 rounded-xl border-[1px] border-solid border-[#E6E6EB] p-8 text-2xl font-normal'
      />
    </div>
  );
};

InputText.propTypes = {
  label: PropTypes.string,
  placeholder: PropTypes.string,
  type: PropTypes.string,
  value: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
  onChange: PropTypes.func,
  className: PropTypes.string,
};

export default InputText;
