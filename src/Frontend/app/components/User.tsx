export default function User(props : {role: string}) {

	let color;

	if(props.role == 'administrador' || props.role == 'terapeuta') {
		color = '#E7343F';
	} else  if (props.role == 'paciente') {
		color = '#12B76A';
	}

	return (
		<div className='flex gap-3'>
			<div className={`w-10 h-10 rounded-full bg-[${color}] bg-opacity-[0.15] flex justify-center items-center`}>
				<p className={`text-base font-medium text-[${color}]`}>AS</p>
			</div>
			<div className='flex flex-col'>
				<p className='text-sm font-medium text-[#101828]'>Ana Carolina</p>
				<p className='text-sm font-normal text-[#667085]'>@anacarolina</p>
			</div>
		</div>
	)
}