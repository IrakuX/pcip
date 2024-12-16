<template>
    <div class="flex flex-col items-center justify-center min-h-screen">
        <div class="hidden lg:block">
            <NuxtImg src="assets/img/logo.png" alt="Image" sizes="xs:100vw sm:50vw lg:400px"
                class="h-full w-full object-scale-down dark:brightness-[0.2] dark:grayscale" densities="x1 x2"
                format="webp" />
        </div>
        <div class="flex items-center justify-center py-12">
            <slot />
        </div>
    </div>
</template>
