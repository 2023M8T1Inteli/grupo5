'use client'

export interface UserProps {
	name: string;
	username: string;
}

export default function User(props : { name: string, username: string }) {

	const initials = props.name.split(' ').map((word) => word[0]).join('')

	return (
		<div className='flex gap-3'>
			<div className={`w-10 h-10 rounded-full bg-opacity-[0.15] flex justify-center items-center`}>
				<p className={`text-base font-medium`}>{initials[0] + initials[1]}</p>
			</div>
			<div className='flex flex-col'>
				<p className='text-sm font-medium text-[#101828]'>{props.name}</p>
				<p className='text-sm font-normal text-[#667085]'>@{props.username}</p>
			</div>
		</div>
	)
}