<template>
    <main>
        <section class="bg-[#f1f1f1]">
            <div class="container flex flex-col items-center gap-10 py-20 lg:flex-row">
                <div class="flex-1 order-2 text-center lg:order-1 lg:text-left">
                    <h1 class="mb-6 text-4xl font-extrabold lg:text-6xl text-balance">
                        Master the Kitchen with Ease: Unleash Your Inner Chef Today!
                    </h1>
                    <p class="mb-8 text-xl lg:text-2xl text-balance">
                        Discover recipes helping you to find the easiest way to cook.
                    </p>
                    <button class="self-start px-4 py-2 text-lg text-white rounded-md cursor-pointer bg-dodgeroll-gold">
                        Browse Recipes
                    </button>
                </div>
                <div class="flex-1 order-1 lg:order-2">
                    <NuxtImg sizes="xs:100vw sm:667px" src="/nuxt-course-hero.png" format="webp" densities="x1"
                        alt="" />
                </div>
            </div>
        </section>
    </main>
</template>
<script setup lang="ts">
definePageMeta({
    layout: 'default'
})
</script>
