import React, { forwardRef, useEffect } from 'react';
import PropTypes from 'prop-types';
import { FieldError, Merge } from 'react-hook-form';

export interface InputTextProps {
  label?: string;
  placeholder?: string;
  type?: string;
  value?: string | number;
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
  className?: string;
  shortcut?: string;
  onUnregister?: () => void;
  name?: string;
  error?: FieldError | Merge<FieldError, { message: string }>;
	disabled?: boolean;
}

const InputText = forwardRef<HTMLInputElement, InputTextProps>(({ label, placeholder, type = 'text', value, onChange, className, shortcut, onUnregister, name, error, disabled }, ref) => {
  useEffect(() => {
    return () => {
      onUnregister?.();
    }
  }, [onUnregister]);

  return (
    <div className={'flex flex-col gap-2' + ' ' + className}>
      {label && (
        <div className='flex items-center justify-between'>
          <label className='text-xl text-[#909090]' htmlFor={name}>{label}</label>
          <span className='text-sm text-[#909090] bg-[#f6f8fa] p-1 rounded'>{shortcut}</span>
        </div>
      )}
      <input
	  	id={name}
        name={name}
        type={type}
        placeholder={placeholder}
        value={value}
        onChange={onChange}
        ref={ref}
				disabled={disabled ? true : false}
        className='w-full h-24 rounded-xl border-[1px] border-solid border-[#E6E6EB] p-8 text-2xl font-normal disabled:bg-[#F6F8FA] disabled:text-[#909090] disabled:cursor-not-allowed'
      />
	  {error && <span className="text-red-500 text-sm">{error.message}</span>} {/* Display error message */}
    </div>
  );
});

InputText.displayName = "InputText";

InputText.propTypes = {
	label: PropTypes.string,
	placeholder: PropTypes.string,
	type: PropTypes.string,
	value: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
	onChange: PropTypes.func,
	className: PropTypes.string,
	shortcut: PropTypes.string,
	onUnregister: PropTypes.func,
	name: PropTypes.string,
  };
  
  export default InputText;
