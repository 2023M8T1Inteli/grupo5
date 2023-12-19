'use client'
import Image from "next/image";
import Logout from '../../public/logout.svg'

export default function LogoutButton() {

	function getUserOut() {
		localStorage.removeItem('token')
		window.location.href = '/'
	}

	return(
		<button type="button" className='flex gap-4 items-center cursor-pointer hover:scale-105 duration-300' onClick={getUserOut}>
			<Image src={Logout} alt='Sair' width={24} height={24} className="m-3"/>
			<span>Sair</span>
		</button>
	)
}