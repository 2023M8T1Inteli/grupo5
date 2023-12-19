export default function Modal({ children }: { children: any }) {
	return (
		<div className='fixed top-0 left-0 w-[100vw] h-[100vh] bg-white bg-opacity-90 flex justify-center items-center'>
			<div className='flex flex-col gap-16 justify-center items-center w-[40rem] bg-white rounded-3xl p-9 border-solid border-[1px] border-[#E6E6EB]'>
				{children}
			</div>
		</div>
	)
}