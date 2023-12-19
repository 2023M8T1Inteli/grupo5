import React, { forwardRef, useEffect } from 'react';
import PropTypes from 'prop-types';
import { FieldError, Merge } from 'react-hook-form';

export interface Option {
	value: string | number;
	label: string;
  }
  
  export interface InputSelectProps {
	label?: string;
	options: Option[];
	onChange?: (e: React.ChangeEvent<HTMLSelectElement>) => void;
	className?: string;
	shortcut?: string;
	onUnregister?: () => void;
	name: string;
	error?: FieldError | Merge<FieldError, { message: string }>;
  }
  

const InputSelect = forwardRef<HTMLSelectElement, InputSelectProps>(
	(
		{ label, options, onChange, className, shortcut, onUnregister, name, error },
		ref
	) => {
		useEffect(() => {
			return () => {
				onUnregister?.();
			};
		}, [onUnregister]);

		return (
			<div className={'flex flex-col gap-2' + ' ' + className}>
				{label && (
					<div className='flex items-center justify-between'>
						<label className='text-xl text-[#909090]' htmlFor={name}>
							{label}
						</label>
						<span className='text-sm text-[#909090] bg-[#f6f8fa] p-1 rounded'>
							{shortcut}
						</span>
					</div>
				)}
				<select
					id={name}
					name={name}
					onChange={onChange}
					ref={ref}
					className='w-full h-24 rounded-xl border-[1px] border-solid border-[#E6E6EB] p-8 text-2xl font-normal appearance-none cursor-pointer pl-5 pr-16'
				>
					<option value='' disabled selected>Selecione uma opção</option>
					{options.map((option, index) => (
						<option key={index} value={option.value}>
							{option.label}
						</option>
					))}
				</select>
				{error && <span className="text-red-500 text-sm">{error.message}</span>}
			</div>
		);
	}
);

InputSelect.displayName = 'InputSelect';

export default InputSelect;
