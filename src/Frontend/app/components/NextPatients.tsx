import PropTypes from 'prop-types';

function NextPatients(props : {className?: string, name: string, hour: string, data: string, linha: any} ) {
  return (
    <div className='grid space-y-2 gap-2 mb-6'>
        <h1 className={'text-4xl font-bold text-black' + '' + props.className}>{props.name}</h1>
        <h2 className={'text-2xl opacity-40 text-black leading-tight' + ' ' + props.className}>{props.hour}</h2>
        <h3 className={'text-1xl opacity-40 text-black' + ' ' + props.className}>{props.data}</h3>
        <h3 className={'w-[510px] h-2 justify-center items-center inline-flex bg-red-500 rounded' + ' ' + props.className}>{props.linha}</h3>
    </div>
  )
}

export default NextPatients;
